.PHONY: format build test run
FILE ?= file_name

format:
	@ \
	cd core ; \
	dotnet format --verbosity normal ; cd .. ; \
	cd ui/module ; npm run lint -- --fix ; cd .. ; \
	cd specs ; npm run lint -- --fix ; cd ../.. ; \
	cd docs/.theme ; npm run lint -- --fix ; cd ../..
fix:
	@ \
	if echo "$(FILE)" | grep -q "^src"; then \
		cd ui/module && npx eslint $(subst ui/module/,,$(FILE)) --fix; \
	elif echo "$(FILE)" | grep -q "^test"; then \
		cd ui/specs && npx eslint $(subst ui/specs/,,$(FILE)) --fix; \
	fi
install:
	@ \
	cd docs/.theme ; npm i ; cd ../.. ; \
	cd docs/.theme ; npm ci ; cd ../.. ; \
	cd ui/module ; npm i ; cd ../.. ; \
	cd ui/module ; npm ci ; cd ../.. ; \
	cd ui/specs ; npm i ; cd ../.. ; \
	cd ui/specs ; npm ci ; cd ../.. ; \
	cd core/test/Baked.Test.Recipe.Service.LoadTest ; npm i ; cd ../../.. ; \
	cd core/test/Baked.Test.Recipe.Service.LoadTest ; npm ci ; cd ../../.. ; \
	cd core/test/Baked.Test.Recipe.Service.StubApi ; npm i ; cd ../../.. ; \
	cd core/test/Baked.Test.Recipe.Service.StubApi ; npm ci ; cd ../../..
build:
	@ \
	cd ui/module ; npm run build ; cd ../.. ; \
	cd core ; dotnet build
test:
	@ \
	cd core ; dotnet test --logger quackers ; cd .. ; \
	cd ui/specs ; BUILD_SILENT=1 npm test ; cd ../..
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
		cd ui/specs ; \
		npm run dev ; \
		cd ../.. ; \
	fi ; \
	if test $$app -eq "3" ; then \
		docker compose up --build ; \
	fi ; \
	if test $$app -eq "4" ; then \
		cd ./docs ; \
		make run ; \
	fi
