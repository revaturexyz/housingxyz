## Technologies Used

- Angular 8.1.3
- Azure AD
- Docker

## Seting up Authentication

 Open the src/enviornments folder. In both the environment.prod.ts and environment.ts files include the tenant and clientId of the Azure AD account you would like to use as follows.  

 export const environment = {  
   production: false,  
   tenant: "your tenant",  
   clientId: "your clientId",  
   extraQueryParameter: 'nux=1', // This is optional  
   endpoints: {  
   "http://localhost:4200": "your clientId"  
    }  
 };  


## Run the Application Using Docker Compose

 Open a command prompt navigate to the housingxyz folder and run the following commands  

 docker build -t housingxyz -f ./.docker/dockerfile .  

 docker stack deploy -c ./.docker.dockerup.yaml housingUI  

 then open your browser and go to localhost:10080  

## Link to Github

[https://github.com/revaturexyz/housingxyz](https://github.com/revaturexyz/housingxyz)

