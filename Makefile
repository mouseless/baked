.PHONY: build run test

build:
	dotnet build
run:
	@ \
	echo "(1) Blueprints.Service" ; \
	echo "(2) Do.Docs" ; \
	echo "" ; \
	echo "Please select 1-2: " ; \
	read app ; \
	if test $$app -eq "1" ; then \
		dotnet run --project test/blueprints/Do.Blueprints.Service.Test ; \
	fi ; \
	if test $$app -eq "2" ; then \
		cd ./docs ; \
		make run ; \
	fi
test:
	dotnet test
