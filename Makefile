.PHONY: format build test run
FILE ?= file_name

format:
	@ \
	dotnet format --verbosity normal ; \
	cd ui/src ; npm run lint -- --fix ; cd ../../.. ; \
	cd ui/test ; npm run lint -- --fix ; cd ../../.. ; \
	cd docs/.theme ; npm run lint -- --fix ; cd ../..
fix:
	@ \
	if echo "$(FILE)" | grep -q "^src"; then \
		cd ui/src && npx eslint $(subst ui/src/,,$(FILE)) --fix; \
	elif echo "$(FILE)" | grep -q "^test"; then \
		cd ui/test && npx eslint $(subst ui/test/,,$(FILE)) --fix; \
	fi
install:
	@ \
	cd docs/.theme ; npm i ; cd ../.. ; \
	cd docs/.theme ; npm ci ; cd ../.. ; \
	cd ui/src ; npm i ; cd ../../.. ; \
	cd ui/src ; npm ci ; cd ../../.. ; \
	cd ui/test ; npm i ; cd ../../.. ; \
	cd ui/test ; npm ci ; cd ../../.. ; \
	cd core/test/service/load-test ; npm i ; cd ../../../.. ; \
	cd core/test/service/load-test ; npm ci ; cd ../../../.. ; \
	cd core/test/service/stub-api-dependency ; npm i ; cd ../../../.. ; \
	cd core/test/service/stub-api-dependency ; npm ci ; cd ../../../..
build:
	@ \
	cd ui/src ; npm run build ; cd ../../.. ; \
	dotnet build
test:
	@ \
	dotnet test --logger quackers ; \
	cd ui/test ; BUILD_SILENT=1 npm test ; cd ../../..
coverage:
	@ \
	cd core ; \
	rm -rdf .coverage ; \
	dotnet test -c Release --collect:"XPlat Code Coverage" --logger trx --results-directory .coverage --settings test/runsettings.xml ; \
	dotnet reportgenerator -reports:.coverage/*/coverage.cobertura.xml -targetdir:.coverage/html ; \
	open .coverage/html/index.html
run:
	@ \
	echo "(1) Recipe.Service (Development)" ; \
	echo "(2) Recipe.UI (Development)" ; \
	echo "(3) Recipe.* (Production)" ; \
	echo "(4) Docs" ; \
	echo "" ; \
	echo "Please select 1-4: " ; \
	read app ; \
	if test $$app -eq "1" ; then \
		dotnet run --project core/test/Baked.Test.Recipe.Service.Application ; \
	fi ; \
	if test $$app -eq "2" ; then \
		cd ui/test ; \
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
