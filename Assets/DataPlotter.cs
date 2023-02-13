using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataPlotter : MonoBehaviour
{
    // File we would like to read
    public string file;

    // Data from csv
    private List<Dictionary<string, object>> dataList;

    // Indices for columns from csv
    public int columnX = 0;
    public int columnY = 2;
    public int columnZ = 1;

    // Full column names
    public string xName; // eastings
    public string yName; // height
    public string zName; // northings

    // Instantiate prefab to plot data points
    public GameObject PointPrefab;

    // Object which will contain instantiated prefabs in hiearchy
    public GameObject DataPointParent;

    // Tile Coordinates
    public int X = 500000; // easting of tile
    public int Y = 100000; // northing of tile

    public float plotScale = 1;

    // Read file
    void Start()
    {
        dataList = CSVReader.Read(file);
        List<string> columns = new List<string>(dataList[0].Keys);
        Debug.Log("There are " + columns.Count + " columns in the file.");
        foreach (string key in columns)
            Debug.Log("Column name is " + key);

        // Name variables
        xName = columns[columnX];
        yName = columns[columnY];
        zName = columns[columnZ];

        for (var i = 0; i < dataList.Count; i++)
        {
            dataList[i][xName] = System.Convert.ToSingle(dataList[i][xName]); //- 500000;
            dataList[i][zName] = System.Convert.ToSingle(dataList[i][zName]); //- 100000;
        }


        // Get maxes of each axis
        float xMax = FindMaxValue(xName);
        float yMax = FindMaxValue(yName);
        float zMax = FindMaxValue(zName);

        // Get minimums of each axis
        float xMin = FindMinValue(xName);
        float yMin = FindMinValue(yName);
        float zMin = FindMinValue(zName);

       
        

        //instantiate prefab with data points from csv
        for (var i = 0; i < dataList.Count; i++)
        {

            // Get value in poinList at ith "row", in "column" Name, normalize
            //float x =
            //(System.Convert.ToSingle(dataList[i][xName]) - xMin) / (xMax - xMin);

            //float y =
            //(System.Convert.ToSingle(dataList[i][yName]) - yMin); // / (yMax - yMin)

            //float z =
            //(System.Convert.ToSingle(dataList[i][zName]) - zMin) / (zMax - zMin);


            float x = System.Convert.ToSingle(dataList[i][xName]) - 500000;
            float y = 10;
            //float y = System.Convert.ToSingle(dataList[i][yName]);
            float z = System.Convert.ToSingle(dataList[i][zName]) - 100000;
            //Debug.Log("DATA POINT X IS: " + x);
            //Debug.Log("DATA POINT Y IS: " + y);
            //Debug.Log("DATA POINT Z IS: " + z);
            
            // Instantiate as gameobject variable so that it can be manipulated within loop
            GameObject dataPoint = Instantiate(
                    PointPrefab,
                    new Vector3(x, y, z), //* plotScale,
                    Quaternion.identity);

            // Make dataPoint child of PointHolder object 
            //dataPoint.transform.parent = DataPointParent.transform;

            // Assigns original values to dataPointName
            string dataPointName =
                dataList[i][xName] + " "
                + dataList[i][yName] + " "
                + dataList[i][zName];

            // Assigns name to the prefab
            //dataPoint.transform.name = dataPointName;
        }

        Instantiate(
                    PointPrefab,
                    new Vector3(16000, 150, 40000), //* plotScale,
                    Quaternion.identity);
        Instantiate(
                    PointPrefab,
                    new Vector3(16000, 150, 41000), //* plotScale,
                    Quaternion.identity);
        Instantiate(
                    PointPrefab,
                    new Vector3(17000, 150, 40000), //* plotScale,
                    Quaternion.identity);
        Instantiate(
                    PointPrefab,
                    new Vector3(17000, 150, 41000), //* plotScale,
                    Quaternion.identity);
    }

    private float FindMaxValue(string columnName)
    {
        //set initial value to first value
        float maxValue = Convert.ToSingle(dataList[0][columnName]);

        //Loop through Dictionary, overwrite existing maxValue if new value is larger
        for (var i = 0; i < dataList.Count; i++)
        {
            if (maxValue < Convert.ToSingle(dataList[i][columnName]))
                maxValue = Convert.ToSingle(dataList[i][columnName]);
        }

        //Spit out the max value
        return maxValue;
    }

    private float FindMinValue(string columnName)
    {

        float minValue = Convert.ToSingle(dataList[0][columnName]);

        //Loop through Dictionary, overwrite existing minValue if new value is smaller
        for (var i = 0; i < dataList.Count; i++)
        {
            if (Convert.ToSingle(dataList[i][columnName]) < minValue)
                minValue = Convert.ToSingle(dataList[i][columnName]);
        }

        return minValue;
    }
}
