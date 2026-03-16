import requests
from datetime import datetime, timezone


#These variables store the station ids for the stations closes to the specialisterne offices
station_id_ballerup = "06181" #jæbersborg station
station_id_odense = "06126" #Årslev station
station_id_aarhus = "06072" #Ødum station
start_time= "2026-03-09T00:00:00Z"
end_time = ".."
parameter_id = "temp_dry"
pull_time = datetime.now(timezone.utc)
pull_time = pull_time.strftime("%Y-%m-%dT%H:%M:%SZ")

if __name__ == "__main__":
    resp = requests.get(f"""https://opendataapi.dmi.dk/v2/metObs/collections/observation/items?parameterId={parameter_id}&limit=3&stationId={station_id_ballerup}&datetime={start_time}/{end_time}&sortorder=observed,DESC""")
    data = resp.json()
    temperature_values = []
    dates = []
    count=0

    filtered_data = [{
        "id": feature["id"],
        "value": feature["properties"]["value"],
        "observed": feature["properties"]["observed"],
        "pulled": pull_time
                      } for feature in data["features"]]

    for observation in filtered_data:
         print(observation)

    #API = DMIAPI()
    #x, data = API.pull_datetime(station_id_ballerup, "temp_dry",limit=1, start_time=start_time)
    #print(data)

    # for feature in data["features"]:
    #     temperature = feature["properties"]["value"]
    #     temperature_values.append(temperature)
    #     date = feature["properties"]["observed"]
    #     dates.append(date)
    #     count+=1

    # plt.figure(figsize=(14, 7))
    # plt.scatter(dates, temperature_values, alpha=0.6)
    # plt.show()
