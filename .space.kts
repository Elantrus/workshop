/**
* JetBrains Space Automation
* This Kotlin-script file lets you automate build activities
* For more info, see https://www.jetbrains.com/help/space/automation.html
*/

job("Hello World!") {
    container("elantrus.registry.jetbrains.space/p/workshop.backend/Workshop.API/Dockerfile")
}
