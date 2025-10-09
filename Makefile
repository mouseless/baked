.PHONY: format build test run
FILE ?= file_name

format:
	@ \
	cd core ; dotnet format --verbosity normal ; cd .. ; \
	cd ui ; npm run lint -- --fix ; cd .. ; \
	cd docs/.theme ; npm run lint -- --fix ; cd ../..
fix:
	@ \
	if echo "$(FILE)" then \
		cd ui && npx eslint $(subst ui/,,$(FILE)) --fix; \
	elif echo "$(FILE)" | grep -q "^test"; then \
		cd ui/specs && npx eslint $(subst ui/specs/,,$(FILE)) --fix; \
	fi
install:
	@ \
	cd docs/.theme ; npm i ; npm ci ; cd ../.. ; \
	cd ui ; npm i ; npm ci ; cd .. ; \
	cd core/test/Baked.Test.Recipe.Service.LoadTest ; npm i ; npm ci ; cd ../../.. ; \
	cd core/test/Baked.Test.Recipe.Service.StubApi ; npm i ; npm ci ; cd ../../..
build:
	@ \
	cd ui ; npm run build:development ; cd .. ; \
	cd core ; dotnet build
test:
	@ \
	cd core ; dotnet test --logger quackers ; cd .. ; \
	cd ui ; BUILD_SILENT=1 npm test ; cd ..
coverage:
	@ \
	cd core ; \
	rm -rdf .coverage ; \
	dotnet test -c Release --collect:"XPlat Code Coverage" --logger trx --results-directory .coverage --settings test/runsettings.xml ; \
	dotnet reportgenerator -reports:.coverage/*/coverage.cobertura.xml -targetdir:.coverage/html ; \
	open .coverage/html/index.html ; \
	cd .. ;
run:
	@ \
	echo "(1) Recipe.Service (Development)" ; \
	echo "(2) Specs (Development)" ; \
	echo "(3) Recipe.* (Production)" ; \
	echo "(4) Docs" ; \
	echo "" ; \
	echo "Please select 1-4: " ; \
	read app ; \
	if test $$app -eq "1" ; then \
		dotnet run --project core/test/Baked.Test.Recipe.Service.Application ; \
	fi ; \
	if test $$app -eq "2" ; then \
		cd ui ; \
		npm run dev ; \
		cd .. ; \
	fi ; \
	if test $$app -eq "3" ; then \
		docker compose up --build ; \
	fi ; \
	if test $$app -eq "4" ; then \
		cd docs ; \
		make run ; \
		cd ..
	fi
