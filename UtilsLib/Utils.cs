namespace UtilsLib
{
    public static class Utils
    {
        public static string FindProgramPath(string programName)
        {
            string programPath = string.Empty;

            string programFilesPath = Environment.GetEnvironmentVariable("ProgramFiles");
            string programFilesx86Path = Environment.GetEnvironmentVariable("ProgramFiles(x86)");

            string[] searchPaths = { programFilesPath, programFilesx86Path };

            foreach (string path in searchPaths)
            {
                if (!string.IsNullOrEmpty(path))
                {
                    string fullPath = Path.Combine(path, programName, $"{programName}.exe");
                    if (File.Exists(fullPath))
                    {
                        programPath = fullPath;
                        break;
                    }
                }
            }

            if(string.IsNullOrEmpty(programPath)) 
            {
                string[] pathDirs = Environment.GetEnvironmentVariable("PATH").Split(';');

                foreach (string pathDir in pathDirs)
                {
                    string fullPath = Path.Combine(pathDir, programName + ".exe");
                    if (File.Exists(fullPath))
                    {
                        return fullPath;
                    }
                }
            }

            return programPath;
        }
    }

    
}
