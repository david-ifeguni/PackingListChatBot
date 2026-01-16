# PackingListChatBot

## Project Overview
My chatbot recommends a travel-specific, vacation packing list, utilizing Azure OpenAI LLM models via the Semantic Kernel

It uses:  
* User's desired destination 
* Travel dates 
* Planned or inferred activities 

To generate a tailored packing list, as well as explanations for all the recommendations. 

Additionally, it supports multi-turn conversations, enabling users to provide this information incrementally.

## Setup Instructions
To run the program, you must have a valid Azure OpenAI Model deployment. You will need the model deployment name, the api key and the base URL of the Target URI (i.e. https://name-####-region.cognitiveservices.azure.com)

### 1) Open Windows Powershell
### 2) Run the three commands belows

- setx AZURE_OPENAI_ENDPOINT "YOUR_AZURE_OPENAI_ENDPOINT"
- setx AZURE_OPENAI_API_KEY "YOUR_AZURE_OPENAI_API_KEY"
- setx AZURE_OPENAI_DEPLOYMENT_NAME "YOUR_AZURE_OPENAI_DEPLOYMENT_NAME"

### 3) Download the zip file of the latest release and extract all contents
### 4) In the unzipped folder, open the "publish" folder
### 5) Select "PackingListChatBot.CLI.exe"
