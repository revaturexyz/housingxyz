## Technologies Used

- Angular 8.1.3
- Azure AD
- Docker

## Setting up Authentication

## Run the Application Using Docker Compose

 Open a command prompt navigate to the housingxyz folder and run the following commands

 docker build -t housingxyz -f ./.docker/dockerfile .

 docker stack deploy -c ./.docker.dockerup.yaml housingUI

 then open your browser and go to localhost:10080

## Link to Github

[https://github.com/revaturexyz/housingxyz](https://github.com/revaturexyz/housingxyz)
