We will create a simple application with dotnet-core

we will use the images created here kubernetes_chat_example [Vault-example](https://github.com/OktaySavdi/vault)


**Build**
```ruby
docker build -t loginproject -f Dockerfile .
```
**Run**
```ruby
docker run -d -p 5000:80 --name myapp loginproject
```
**Call**
```ruby
curl http://localhost:5000/login
 ```
 **Login Info**
 ```
 User Name : admin
 Password  : password
  ```
