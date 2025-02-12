.PHONY: format build test run

format:
	@ dotnet format --verbosity normal
build:
	@ dotnet build
test:
	@ dotnet test
coverage:
	@ \
	rm -rdf .coverage ; \
	dotnet test -c Release --collect:"XPlat Code Coverage" --logger trx --results-directory .coverage --settings test/runsettings.xml ; \
	dotnet reportgenerator -reports:.coverage/*/coverage.cobertura.xml -targetdir:.coverage/html ; \
	open .coverage/html/index.html
run:
	@ \
	echo "(1) Recipe.Service (Development)" ; \
	echo "(2) Recipe.Service (Production)" ; \
	echo "(3) Recipe.Admin (Development)" ; \
	echo "(4) Docs" ; \
	echo "" ; \
	echo "Please select 1-4: " ; \
	read app ; \
	if test $$app -eq "1" ; then \
		dotnet run --project test/recipe/Baked.Test.Recipe.Service.Application ; \
	fi ; \
	if test $$app -eq "2" ; then \
		docker compose up --build ; \
	fi ; \
	if test $$app -eq "3" ; then \
		cd test/recipe/admin ; \
		npm run dev ; \
		cd ../../.. ; \
	fi ; \
	if test $$app -eq "4" ; then \
		cd ./docs ; \
		make run ; \
	fi
