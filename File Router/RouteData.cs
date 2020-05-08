using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Router {

    public class RouteData {
        public List<Route> routes;
        public MainWindow mainWindow;

        public RouteData(MainWindow mainWindow) {
            this.mainWindow = mainWindow;

            routes = new List<Route>();
        }

        /// <summary>
        /// Adds a new route and updates the route name list
        /// </summary>
        /// <param name="name"></param>
        /// <param name="sourcePath"></param>
        /// <param name="destinationPath"></param>
        public void AddRoute(String name, string sourcePath, string destinationPath) {
            Route newRoute = new Route(name, sourcePath, destinationPath);

            routes.Add(newRoute);

            mainWindow.updateRouteNameList();
            saveRoutesToFile();
        }

        /// <summary>
        /// Checks if the route name is in use excluding the name currently being edited
        /// </summary>
        /// <param name="name"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public bool routeNameInUse(string name, int exclude) {
            for (int i = 0; i < routes.Count; i++) {
                if (i != exclude) {
                    if (routes[i].name.Equals(name)) {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if a route name is already in use
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool routeNameInUse(string name) {
            foreach (Route route in routes) {
                if (route.name.Equals(name)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the source directory is already in use excluding the one being edited
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="exclude"></param>
        /// <returns></returns>
        public bool sourceDirectoryInUse(string directory, int exclude) {
            for (int i = 0; i < routes.Count; i++) {
                if (i != exclude && routes[i].sourceFolder.Equals(directory)) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the source directory is already in use
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public bool sourceDirectoryInUse(string directory) {
            foreach (Route route in routes) {
                if (route.sourceFolder.Equals(directory)) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Saves routes to a json file
        /// </summary>
        public void saveRoutesToFile() {
            if (!Directory.Exists("Data")) {
                Directory.CreateDirectory("Data");
            }

            string jsonString = JsonConvert.SerializeObject(routes, Formatting.Indented);
            File.WriteAllText(@"Data\RouteSaves.json", jsonString);
        }

        /// <summary>
        /// Load routes from a json file and updates the route name list
        /// </summary>
        public void loadRoutesFromFile() {
            if (File.Exists(@"Data\RouteSaves.json")) {
                string jsonResult = File.ReadAllText(@"Data\RouteSaves.json");
                routes = JsonConvert.DeserializeObject<List<Route>>(jsonResult);
                mainWindow.updateRouteNameList();
            }
        }
    }
}
