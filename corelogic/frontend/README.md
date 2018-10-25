<h1 id="argon-design-system">FrontEnd for CoreLogic test</h1>

<h2 id="download-and-installation">Dependencies</h2>

<p>This project is using Argon UI Kit based on Bootstrap 4</p>
<p>To install dependencies, simply clone the repository and run <pre>npm install</pre></p>
<p>To run a local server with hot reload, simply run <pre>gulp</pre></p>

<h3 id="download-and-installation">Gotcha</h3>
<p>The gulp local server <strong>MUST</strong> run on the port 3000, because the API server also running on localhost is allowing cross-origin on port localhost:3000</p>

<h2 id="download-and-installation">Based on Argon UI Kit / Theme</h2>

<ul>
  <li><a href="https://github.com/creativetimofficial/argon-design-system/archive/master.zip">Download from Github</a></li>
</ul>

<h2 id="file-structure">Project Structure</h2>

<div class="highlighter-rouge"><div class="highlight"><pre class="highlight"><code>argon/
├── README.md
├── assets/
  ├── css/
  │   ├── argon.css
  │   ├── argon.css.map
  │   ├── argon.min.css
  │   ├── argon.min.css.map
  └── img/
  │   ├── argon/
  │   ├── brand/
  │   ├── icons/
  │   ├── ill/
  └── js/
  │   ├── argon.js
  │   └── argon.min.js
  └── scss/
  │   ├── bootstrap/
  │   ├── custom/
  │   ├── argon.scss
  └── vendor/
      ├── bootstrap/
      ├── bootstrap-datepicker/
      ├── font-awesome/
      ├── headroom/
      ├── jquery/
      ├── nouislider/
      ├── nucleo/
      ├── popper/

</code></pre></div></div>

<h2 id="browser-support">Browser Support</h2>

<p>At present, Argon officially aim to support the last two versions of the following browsers:</p>

<p><img src="https://s3.amazonaws.com/creativetim_bucket/github/browser/chrome.png" width="64" height="64" />
<img src="https://s3.amazonaws.com/creativetim_bucket/github/browser/firefox.png" width="64" height="64" />
<img src="https://s3.amazonaws.com/creativetim_bucket/github/browser/edge.png" width="64" height="64" />
<img src="https://s3.amazonaws.com/creativetim_bucket/github/browser/safari.png" width="64" height="64" />
<img src="https://s3.amazonaws.com/creativetim_bucket/github/browser/opera.png" width="64" height="64" /></p>
