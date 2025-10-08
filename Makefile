.PHONY: format build test run
FILE ?= file_name

format:
	@ \
	dotnet format --verbosity normal ; \
	cd src/recipe/admin ; npm run lint -- --fix ; cd ../../.. ; \
	cd test/recipe/admin ; npm run lint -- --fix ; cd ../../.. ; \
	cd docs/.theme ; npm run lint -- --fix ; cd ../..
fix:
	@ \
	if echo "$(FILE)" | grep -q "^src"; then \
		cd src/recipe/admin && npx eslint $(subst src/recipe/admin/,,$(FILE)) --fix; \
	elif echo "$(FILE)" | grep -q "^test"; then \
		cd test/recipe/admin && npx eslint $(subst test/recipe/admin/,,$(FILE)) --fix; \
	fi
install:
	@ \
	cd docs/.theme ; npm i ; cd ../.. ; \
	cd docs/.theme ; npm ci ; cd ../.. ; \
	cd src/recipe/admin ; npm i ; cd ../../.. ; \
	cd src/recipe/admin ; npm ci ; cd ../../.. ; \
	cd test/recipe/admin ; npm i ; cd ../../.. ; \
	cd test/recipe/admin ; npm ci ; cd ../../.. ; \
	cd test/recipe/service/load-test ; npm i ; cd ../../../.. ; \
	cd test/recipe/service/load-test ; npm ci ; cd ../../../.. ; \
	cd test/recipe/service/stub-api-dependency ; npm i ; cd ../../../.. ; \
	cd test/recipe/service/stub-api-dependency ; npm ci ; cd ../../../..
build:
	@ \
	cd src/recipe/admin ; npm run build ; cd ../../.. ; \
	dotnet build
test:
	@ \
	dotnet test --logger quackers ; \
	cd test/recipe/admin ; BUILD_SILENT=1 npm test ; cd ../../..
coverage:
	@ \
	rm -rdf .coverage ; \
	dotnet test -c Release --collect:"XPlat Code Coverage" --logger trx --results-directory .coverage --settings test/runsettings.xml ; \
	dotnet reportgenerator -reports:.coverage/*/coverage.cobertura.xml -targetdir:.coverage/html ; \
	open .coverage/html/index.html
run:
	@ \
	echo "(1) Recipe.Service (Development)" ; \
	echo "(2) Recipe.Admin (Development)" ; \
	echo "(3) Recipe.* (Production)" ; \
	echo "(4) Docs" ; \
	echo "" ; \
	echo "Please select 1-4: " ; \
	read app ; \
	if test $$app -eq "1" ; then \
		dotnet run --project test/recipe/Baked.Test.Recipe.Service.Application ; \
	fi ; \
	if test $$app -eq "2" ; then \
		cd test/recipe/admin ; \
		npm run dev ; \
		cd ../../.. ; \
	fi ; \
	if test $$app -eq "3" ; then \
		docker compose up --build ; \
	fi ; \
	if test $$app -eq "4" ; then \
		cd ./docs ; \
		make run ; \
	fi
