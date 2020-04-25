Download Selenium Server (Grid) from https://www.selenium.dev/downloads/ 
Download chrome driver from https://chromedriver.chromium.org/

java -jar .\selenium-server-standalone-3.141.59.jar -role hub -hubConfig hubConfig.json
java -jar .\selenium-server-standalone-3.141.59.jar -role node -nodeConfig nodeConfig.json
