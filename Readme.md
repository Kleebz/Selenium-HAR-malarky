*C#
  Use the Program class to run the selenium. This will be ported through a proxy (set as 8080 because that's the default for the mitmproxy tool')
  It will just call a website (hardcoded right now)
  Once you run through the docker setup (below) you can run the C# selenium program and the traffic should show up in the command prompt with docker running
  The requests will also dump to a file defined in the dockerfile
*Docker:
  this docker file holds mitmproxy, used to proxy the http traffic. 
  To build (be in the directory with the dockerfile):
docker build ./ -t [your docker image name here]
note: ignore the square brackets
then run like this:
  ```
docker run -it --rm -p 8080:8080 [your docker image name]
"-it" -> interactive and teletype (docker process will take over the prompt, exit with ctrl-C)
"-p" -> set the port mapping from host:container
optional: you can add a --name [containerName]
```
  This will launch the mitmdump program with instruction to log out to the specified file in the dockerfile
  NOTE: to get the dump file from the docker container to your host machine:
  find the name of the docker container either defined in the optional field described above or in the following command
```
docker ps -as
```
  then initate the copy command
```
docker cp [containerName]:\path\to\dump\from\dockerfile path\to\host\dir
```

cheers :beer: