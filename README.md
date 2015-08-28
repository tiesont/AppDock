# AppDock

AppDock (AD) is a WPF command launcher application, developed as a quick-access tool for commonly-used applications in the computer lab I work in. 

This is an updated version of my AppLauncher (AL) project, upgraded to include a DI framework, Simple Injector. To balance that out, it was downgraded to use Microsoft Access for data storage, since the users of AL are generally adverse to manipulating the XML file AL used for storage.

AD reads in all active records from the LabApplications table and generates a button for each row. If the type is set to "executable", it does a `File.Exists()` on the path given. If said path cannot be found, it will disable the button. We use this feature to provide links to both x86 and x64 versions of some of our applications - if one cannot be reached, the other generally can. If the type is set to "website", it simply exectues the URL when the button is clicked, which will cause Windows to use the default webbrowser to navigate to the site.
