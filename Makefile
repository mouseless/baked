.PHONY: format fix install build test coverage run
FILE ?= file_name
UI_DIR := ui
CORE_DIR := core
DOCS_DIR := docs/.theme
LOADTEST_DIR := core/test/Baked.Test.Recipe.Service.LoadTest
STUBAPI_DIR := core/test/Baked.Test.Recipe.Service.StubApi

format:
	@for dir in $(CORE_DIR) $(UI_DIR) $(DOCS_DIR); do \
		if [ "$$dir" = "$(CORE_DIR)" ]; then \
			dotnet format --verbosity normal -w $$dir; \
		else \
			(cd $$dir && npm run lint -- --fix); \
		fi \
	done
fix:
	@if [ -n "$(FILE)" ]; then \
		npx eslint $(subst ui/,,$(FILE)) --fix; \
	fi
install:
	@for dir in $(DOCS_DIR) $(UI_DIR) $(LOADTEST_DIR) $(STUBAPI_DIR); do \
		(cd $$dir && npm ci); \
	done
build:
	@(cd $(UI_DIR) && npm run build:development)
	@(cd $(CORE_DIR) && dotnet build)
test:
	@(cd $(CORE_DIR) && dotnet test --logger quackers)
	@(cd $(UI_DIR) && BUILD_SILENT=1 npm test)
coverage:
	@(cd $(CORE_DIR) && \
		rm -rf .coverage && \
		dotnet test -c Release --collect:"XPlat Code Coverage" --logger trx --results-directory .coverage --settings test/runsettings.xml && \
		dotnet reportgenerator -reports:.coverage/*/coverage.cobertura.xml -targetdir:.coverage/html && \
		open .coverage/html/index.html)
run:
	@echo "(1) Recipe.Service (Development)"
	@echo "(2) Specs (Development)"
	@echo "(3) Recipe.* (Production)"
	@echo "(4) Docs"
	@read -p "Please select 1-4: " app; \
	case $$app in \
		1) dotnet run --project $(CORE_DIR)/test/Baked.Test.Recipe.Service.Application ;; \
		2) (cd $(UI_DIR) && npm run dev) ;; \
		3) docker compose up --build ;; \
		4) (cd docs && make run) ;; \
		*) echo "Invalid option";; \
	esac