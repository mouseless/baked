.PHONY: build run test

build:
	dotnet build
run:
	@ \
	echo "(1) Do.Docs" ; \
	echo "" ; \
	echo "Please select 1: " ; \
	read app ; \
	if test $$app -eq "1" ; then \
		cd ./docs ; \
		make run ; \
	fi
test:
	dotnet test
