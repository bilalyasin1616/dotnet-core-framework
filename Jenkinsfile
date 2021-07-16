pipeline {
    agent {
        docker {
            image 'mcr.microsoft.com/dotnet/core/sdk:2.1'
            args '--user 0:0 --network host' 
        }
    }
    environment {
        DOTNET_CLI_HOME = "/tmp/DOTNET_CLI_HOME"
        SOURCE_URL= "http://localhost:5555/v3/index.json"
        VERSION= sh (returnStdout: true, script: "grep version vars | cut -d '=' -f2-")
    }
    stages {
        // stage () {

        // }
        stage('Build') {
            steps {
                sh "dotnet restore"
                sh "dotnet build -c Release"
            }
        }
        stage('Create Nuget') {
            steps {
                dir("${env.WORKSPACE}/DW.DAL"){
                    sh """ 
                        sed -i 's/<PKG_VERSION>/${env.VERSION.trim()}/g' ./nugetfile.nuspec
                        dotnet pack ./DW.DAL.csproj -p:NuspecFile=./nugetfile.nuspec  -c Release --no-build -p:IncludeSymbols=true -p:SymbolPackageFormat=snupkg --include-source
                    """
                }
            }
        }
        stage('Upload Nuget') {
            steps {
                script {
                    withCredentials([string(credentialsId: 'nuget-source-key', variable: 'PW1')]) {
                        sh "dotnet nuget push -s ${env.SOURCE_URL} -k ${PW1} ./DW.DAL/bin/Release/DW.Framework.${env.VERSION.trim()}.nupkg"
                    }
                }
            }
        }
    }
}
