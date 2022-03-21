import Chart from "chart.js/auto";
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
    let date7 = Date.parse("01.07.2020");

    const data = {
      labels: [date1, date2, date3, date4, date5, date6, date7],
      datasets: [
        {
          label: "My First dataset",
          backgroundColor: "#8e5ea2",
          borderColor: "#3e95cd",
          fill: false,
          data: [10, 20, 30, 40, 50, 60, 70],
        },
        {
          label: "My Second dataset",
          backgroundColor: "#3e95cd",
          borderColor: "#8e5ea2",
          fill: false,
          data: [70, 60, 50, 40, 30, 20, 10],
        },
        {
          label: "Dataset with point data",
          backgroundColor: "#3cba9f",
          borderColor: "#3cba9f",
          fill: false,
          data: [
            {
              x: date1,
              y: 10,
            },
            {
              x: date2,
              y: 20,
            },
            {
              x: date3,
              y: 30,
            },
            {
              x: date4,
              y: 40,
            },
          ],
        },
      ],
    };
    const config = {
      type: "line",
      data: data,
      options: {
        plugins: {
          title: {
            text: "Chart.js Time Scale",
            display: true,
          },
        },
        scales: {
          x: {
            type: "time",
            time: {
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

    const myChart = new Chart(document.getElementById(chartId), config);
  });

  return (
    <>
      <canvas id={chartId}></canvas>
    </>
  );
};

export default WeightChart;
