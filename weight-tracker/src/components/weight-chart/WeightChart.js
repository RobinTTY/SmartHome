import Chart from "chart.js/auto";
import "chartjs-adapter-date-fns";
import { useEffect } from "react";

const WeightChart = (props) => {
  const chartId = "weight-chart";

  useEffect(() => {
    let date1 = Date.parse("01.01.2020");
    let date2 = Date.parse("01.02.2020");
    let date3 = Date.parse("01.03.2020");
    let date4 = Date.parse("01.04.2020");
    let date5 = Date.parse("01.05.2020");
    let date6 = Date.parse("01.06.2020");
    let date7 = Date.parse("02.07.2020");

    const data = {
      labels: [date1, date2, date3, date4, date5, date6, date7],
      datasets: [
        {
          label: "Weight",
          backgroundColor: "#5580FF",
          borderColor: "#80AAFF",
          fill: false,
          data: [10, 20, 30, 40, 50, 60, 70],
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
              text: "value",
            },
          },
        },
      },
    };

    // construct chart
    new Chart(document.getElementById(chartId), config);
  }, []);

  return (
    <>
      <canvas id={chartId}></canvas>
    </>
  );
};

export default WeightChart;
