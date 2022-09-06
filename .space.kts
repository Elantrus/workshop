job(".NET Core test"){
    container(image = "mcr.microsoft.com/dotnet/sdk:6.0"){
        shellScript {
            content = """
                dotnet test
            """
        }
    }
    
    startOn {
    	gitPush { enabled = true }
    }
}