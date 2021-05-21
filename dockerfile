#base image
FROM mitmproxy/mitmproxy:latest

#create a dir and log file for the dump
RUN mkdir -p /stuff/ && echo " " >> /stuff/outFile.txt

#enter with the -w option to specify the output file
ENTRYPOINT [ "mitmdump", "-w", "/stuff/outFile.txt" ]