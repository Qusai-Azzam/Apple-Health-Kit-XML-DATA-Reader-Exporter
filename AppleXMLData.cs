using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AppleHealthKitExporter
{
    class AppleXMLData
    {
        private string sourceFile;
        private string distinationDirectory;


        private XmlDocument xmldoc;

        /// <summary>
        /// XmlNodeLists to Store Nodes
        /// </summary>
        private XmlNodeList heartRateList;
        private XmlNodeList stepCountList;
        private XmlNodeList standHourList;
        private XmlNodeList sleepAnalysisList;
        private XmlNodeList exerciseTimeList;
        private XmlNodeList distanceWalkingRunningList;
        private XmlNodeList activeEnergyBurnedList;
        private XmlNodeList basalEnergyBurnedList;

        /// <summary>
        /// XML Nodes
        /// </summary>
        private const string HEART_RATE_QUERY = "//HealthData/Record[@type='HKQuantityTypeIdentifierHeartRate']";
        private const string STEP_COUNT_QUERY = "//HealthData/Record[@type='HKQuantityTypeIdentifierStepCount']";
        private const string STAND_HOUR_QUERY = "//HealthData/Record[@type='HKCategoryTypeIdentifierAppleStandHour']";
        private const string SLEEP_ANALYSIS_QUERY = "//HealthData/Record[@type='HKCategoryTypeIdentifierSleepAnalysis']";
        private const string EXECERSISE_TIME_QUERY = "//HealthData/Record[@type='HKQuantityTypeIdentifierAppleExerciseTime']";
        private const string DISTANCE_WALKING_RUNNING_QUERY = "//HealthData/Record[@type='HKQuantityTypeIdentifierDistanceWalkingRunning']";
        private const string ACTIVE_ENERGY_BURNED_QUERY = "//HealthData/Record[@type='HKQuantityTypeIdentifierActiveEnergyBurned']";
        private const string BASAL_ENERGY_BURNED_QUERY = "//HealthData/Record[@type='HKQuantityTypeIdentifierBasalEnergyBurned']";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_sourceFile"></param>
        public AppleXMLData(string _sourceFile)
        {
            sourceFile = _sourceFile;

            this.xmldoc = new XmlDocument();
            this.xmldoc.Load(sourceFile);

            /// Load All Data From XML Document
            this.heartRateList = xmldoc.SelectNodes(HEART_RATE_QUERY);
            this.stepCountList = xmldoc.SelectNodes(STEP_COUNT_QUERY);
            this.standHourList = xmldoc.SelectNodes(STAND_HOUR_QUERY);
            this.sleepAnalysisList = xmldoc.SelectNodes(SLEEP_ANALYSIS_QUERY);
            this.exerciseTimeList = xmldoc.SelectNodes(EXECERSISE_TIME_QUERY);
            this.distanceWalkingRunningList = xmldoc.SelectNodes(DISTANCE_WALKING_RUNNING_QUERY);
            this.activeEnergyBurnedList = xmldoc.SelectNodes(ACTIVE_ENERGY_BURNED_QUERY);
            this.basalEnergyBurnedList = xmldoc.SelectNodes(BASAL_ENERGY_BURNED_QUERY);

        }

        /// <summary>
        /// Distination Directory that Save the OUTPUT Files
        /// </summary>
        public string DistinationDirectory
        {
            get { return distinationDirectory; }
            set { distinationDirectory = value; }
        }

        public void ExportIntoCSVFiles()
        {
            // Export Data
            ExportFile(this.heartRateList, "HR-Data");
            ExportFile(this.stepCountList, "StepCount-Data");
            ExportFile(this.standHourList, "StandHour-Data");
            ExportFile(this.sleepAnalysisList, "SleepAnalysis-Data");
            ExportFile(this.exerciseTimeList, "ExerciseTime-Data");
            ExportFile(this.distanceWalkingRunningList, "DistanceWalkingRunnung-Data");
            ExportFile(this.activeEnergyBurnedList, "ActiveEnergyBurned-Data");
            ExportFile(this.basalEnergyBurnedList, "BasalEnergyBurned-Data");
        }

        public void ExportFile(XmlNodeList list, string fileName)
        {
            try
            {
                string distanceFilePath = string.Format("{0}\\{1}.csv", distinationDirectory, fileName);
                StreamWriter excelFile = new StreamWriter(distanceFilePath, false, Encoding.UTF8);
                string firstLine = string.Format("{0},{1},{2},{3}", "creationDate", "startDate", "endDate", "value");
                excelFile.WriteLine(firstLine);

                foreach (XmlNode node in list)
                {
                    DateTime creationDate = DateTime.Parse(node.Attributes["creationDate"].Value.ToString());
                    DateTime startDate = DateTime.Parse(node.Attributes["startDate"].Value.ToString());
                    DateTime endDate = DateTime.Parse(node.Attributes["endDate"].Value.ToString());
                    string nodeValue = node.Attributes["value"].Value.ToString();
                    string newLine = string.Format("{0},{1},{2},{3}", creationDate, startDate, endDate, nodeValue);
                    excelFile.WriteLine(newLine);
                }
                excelFile.Flush();
                excelFile.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
