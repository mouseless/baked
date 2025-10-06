param (
    [string]$File
)

function Format {
    Write-Host "🔧 Running formatters..."
    dotnet format --verbosity normal

    Push-Location "ui/src/admin"
    npm run lint -- --fix
    Pop-Location

    Push-Location "ui/test/admin"
    npm run lint -- --fix
    Pop-Location

    Push-Location "docs/.theme"
    npm run lint -- --fix
    Pop-Location
}

function Fix {
    if (-not $File) {
        $File = Read-Host "Enter file path (e.g. ui/src/admin/components/App.vue)"
    }

    if ($File -match "^src") {
        Push-Location "ui/src/admin"
        $relative = $File -replace "^ui/src/admin/", ""
        npx eslint $relative --fix
        Pop-Location
    } elseif ($File -match "^test") {
        Push-Location "ui/test/admin"
        $relative = $File -replace "^ui/test/admin/", ""
        npx eslint $relative --fix
        Pop-Location
    } else {
        Write-Host "❌ FILE parametresi geçerli bir path değil."
    }
}

function Install {
    $paths = @(
        "docs/.theme",
        "ui/src/admin",
        "ui/test/admin",
        "core/test/service/load-test",
        "core/test/service/stub-api-dependency"
    )

    foreach ($p in $paths) {
        Push-Location $p
        npm install
        npm ci
        Pop-Location
    }
}

function Build {
    Push-Location "ui/src/admin"
    npm run build
    Pop-Location

    dotnet build
}

function Test {
    dotnet test --logger "quackers"

    Push-Location "ui/test/admin"
    $env:BUILD_SILENT = "1"
    npm test
    Pop-Location
}

function Coverage {
    Push-Location "core"
    if (Test-Path ".coverage") { Remove-Item ".coverage" -Recurse -Force }
    dotnet test -c Release --collect:"XPlat Code Coverage" --logger trx --results-directory .coverage --settings test/runsettings.xml
    dotnet reportgenerator -reports:.coverage/*/coverage.cobertura.xml -targetdir:.coverage/html
    Start-Process ".coverage/html/index.html"
    Pop-Location
}

function Run {
    Write-Host "`n(1) Recipe.Service (Development)"
    Write-Host "(2) Recipe.Admin (Development)"
    Write-Host "(3) Recipe.* (Production)"
    Write-Host "(4) Docs`n"

    $app = Read-Host "Please select 1-4"

    switch ($app) {
        "1" {
            Push-Location "core"
            dotnet run --project "test/Baked.Test.Recipe.Service.Application"
            Pop-Location
        }
        "2" {
            Push-Location "ui/test/admin"
            npm run dev
            Pop-Location
        }
        "3" {
            docker compose up --build
        }
        "4" {
            Push-Location "docs"
            pwsh ./make.ps1
            Pop-Location
        }
        default {
            Write-Host "❌ Invalid selection."
        }
    }
}

function MainMenu {
    Write-Host "(1) format"
    Write-Host "(2) fix"
    Write-Host "(3) install"
    Write-Host "(4) build"
    Write-Host "(5) test"
    Write-Host "(6) coverage"
    Write-Host "(7) run"
    Write-Host "(0) exit"
    Write-Host "==============================="
    $choice = Read-Host "Select a task [0-7]"

    switch ($choice) {
        "1" { Format }
        "2" { Fix }
        "3" { Install }
        "4" { Build }
        "5" { Test }
        "6" { Coverage }
        "7" { Run }
        "0" { exit }
        default {
            Write-Host "❌ Invalid choice"
            Start-Sleep 1
        }
    }

    Write-Host ""
    Read-Host "Press Enter to continue..."
    MainMenu
}

MainMenu
