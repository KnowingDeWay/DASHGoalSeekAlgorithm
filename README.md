This is the technical solution to the DASH Technical challenge.

Assumptions Made for this project:

1. The pronumeral/variable being used in the formula field will be strictly named 'input'
2. The solution being sought after is a REAL solution (i.e. not an imaginary or complex number)
3. Only ONE variable will be used (i.e. the formula is NOT multivariable, for example x + y - 10)
4. Goal Seek in the Excel variant allows for a 0.001 degree of tolerance, so exact solutions are NOT expected in many cases.
5. The back-end system and front-end system will run on the same physical machine
6. Given point (4), the moment a solution yields a result within 0.001 of the desired result, the alogrithm will stop despite the maximum amount of iterations permitted.
7. As per standard security measures, the .env file that is required to execute the front-end project has been omitted, please ask me if you wish to run this solution
on your local machine. The .env file consists of two keys:
	a) VITE_API_BASE_ADDRESS (e.g. "https://localhost:")
	b) VITE_API_PORT (e.g. 80, 3000)

Together, they form the full base url, for example: https://localhost:80

On request, I can provide you with the .env file to put in the root of the goalseekalgorithm.client/gsa-front-end folder.

All external third-party libraries used for this project are covered under the MIT license which allow for free and unconditional personal and commercial use.

Instructions to start project:

1. Open GoalSeekAlgorithm.sln file and select the GoalSeekAlgorithm.Server project and then click the green play button above to run the back-end system.
2. Open the goalseekalgorithm.client/gsa-front-end folder in Visual Studio Code.
3. Go to the top toolbar and then click the Terminal drop down menu and then click 'New Terminal'. Then type 'npm install' to install all required dependencies.
4. After the above packages have been installed, type npm run dev and then press enter.
5. Go to the URL displayed by VITE in the terminal and then Ctrl+Click on the URL to load the UI page.
6. You may now enter values into the form of the UI field, click the purple button when you want to calculate the solution to your equation.
7. Done!

