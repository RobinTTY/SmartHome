import Chart from "chart.js/auto";
import "chartjs-adapter-date-fns";
import { useEffect } from "react";
import WeightData from "../../models/WeightData";

const WeightChart = (props) => {
  const chartId = "weight-chart";

  useEffect(async () => {
    const historicWeightData = await getHistoricWeightData();
    const weightDataParsed = historicWeightData.map(
      (item) => new WeightData(item.id, item.weight, new Date(item.timeStamp))
    );
    const dates = weightDataParsed.map((item) => item.timeStamp);
    const values = weightDataParsed.map((item) => item.weight);

    const data = {
      labels: dates,
      datasets: [
        {
          label: "Weight",
          backgroundColor: "#5580FF",
          borderColor: "#80AAFF",
          fill: false,
          data: values,
        },
      ],
    };

    // config
    const config = {
      type: "line",
      data: data,
      options: {
        plugins: {
          title: {
            text: "Weight over time",
            display: true,
          },
        },
        scales: {
          x: {
            type: "time",
            time: {
              display: true,
              unit: "day",
            },
          },
          y: {
            title: {
              display: true,
              text: "weight in kg",
            },
          },
        },
      },
    };

    // construct chart
    new Chart(document.getElementById(chartId), config);
  }, []);

  const getHistoricWeightData = async () => {
    return fetch("http://localhost:5262/weightData")
      .then((response) => response.json())
      .then((data) => {
        return data;
      });
  };

  return (
    <>
      <canvas id={chartId}></canvas>
    </>
  );
};

export default WeightChart;
