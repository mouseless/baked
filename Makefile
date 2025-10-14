.PHONY: format fix install build test coverage run
FILE ?= file_name

format:
	@(cd core && dotnet format --verbosity normal)
	@(cd ui && npm run lint -- --fix)
	@(cd docs/.theme && npm run lint -- --fix)
fix:
	@if [ -n "$(FILE)" ]; then \
		npx eslint $(subst ui/,,$(FILE)) --fix; \
	fi
install:
	@(cd core/test/Baked.Test.LoadTest && npm i && npm ci)
	@(cd core/test/Baked.Test.StubApi && npm i && npm ci)
	@(cd docs/.theme && npm i && npm ci)
	@(cd ui && npm i && npm ci)
build:
	@(cd ui && npm run build)
	@(cd core && dotnet build)
test:
	@(cd core && dotnet test --logger quackers)
	@(cd ui && BUILD_SILENT=1 npm run test)
coverage:
	@( \
		cd core && \
		rm -rf .coverage && \
		dotnet test -c Release --collect:"XPlat Code Coverage" --logger trx --results-directory .coverage --settings test/runsettings.xml && \
		dotnet reportgenerator -reports:.coverage/*/coverage.cobertura.xml -targetdir:.coverage/html && \
		open .coverage/html/index.html \
	)
run:
	@echo "(1) Service (Development)"
	@echo "(2) UI (Development)"
	@echo "(3) Docker (Production)"
	@echo "(4) Docs"
	@read -p "Please select 1-4: " app; \
	case $$app in \
		1) dotnet run --project core/test/Baked.Test.Application ;; \
		2) (cd ui && npm run dev) ;; \
		3) docker compose up --build ;; \
		4) (cd docs && make run) ;; \
		*) echo "Invalid option";; \
	esac