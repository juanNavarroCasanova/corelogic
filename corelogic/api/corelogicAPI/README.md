<h1>CoreLogic test</h1>

<h2 id="download-and-installation">Dependencies</h2>

<p>Needs installation of Node , npm and .NET CLI</p>

<h3 id="download-and-installation">Gotcha</h3>
<p>Inside of project folder ( corelogicApi ) Run the command  "dotnet run" to check if it is working check
localhost:59489/api/practitioners ( the port must be 59489)</p>

<h2 id="file-structure">Project Structure</h2>

<div class="highlighter-rouge"><div class="highlight"><pre class="highlight"><code>/
├── README.md
├── corelogicAPI/
  ├── Controllers
  │   ├── AppointmentsController
  │   ├── PractitionersController
  │   ├── ReportController
  │   ├── argo
  └── Data/
  │   ├── appointments
  │   ├── practisioners  
  └── Models/
  │   ├── AppointmentModel
  │   ├── CostRevenueModel
  |   ├── FilterModel
  │   ├── PractisionerModel
  |   ├── ReportModel

 
