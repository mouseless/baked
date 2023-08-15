.PHONY: build run test

build:
	dotnet build
run:
	@ \
	echo "(1) Blueprints.Service (Development)" ; \
	echo "(2) Blueprints.Service (Production)" ; \
	echo "(3) Do.Docs" ; \
	echo "" ; \
	echo "Please select 1-2: " ; \
	read app ; \
	if test $$app -eq "1" ; then \
		dotnet run --project test/blueprints/Do.Test.Blueprints.Service.Application ; \
	fi ; \
	if test $$app -eq "2" ; then \
		docker compose up --build ; \
	fi ; \
	if test $$app -eq "3" ; then \
		cd ./docs ; \
		make run ; \
	fi
test:
	dotnet test
