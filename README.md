![image](https://github.com/YudinDP/Data-Structures-And-Algorithms/assets/146605173/01d3a8a1-7740-4b5e-a0c3-4bad0e9972fc)
@startuml

class ClassTempAndWeather{
- double degrees
- string weather
- char measure
- string path

+double getDegrees()
+string getWeather()
+char getMeasure()

+string ToString()
+string ToStringObject()

+void setDegrees(double newDegrees)
+void setMeasure(char newMeasure)
+void setWeather(string new Weather)
+void addDegrees(double change)

+void Test()
}
@enduml
