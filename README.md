We will create a simple application with dotnet-core

we will use the images created here kubernetes_chat_example [Vault-example](https://github.com/OktaySavdi/vault)


#  Build

    docker build -t loginproject -f Dockerfile .

# Run
    docker run -d -p 5000:80 --name myapp loginproject

# Call
    curl http://localhost:5000/login