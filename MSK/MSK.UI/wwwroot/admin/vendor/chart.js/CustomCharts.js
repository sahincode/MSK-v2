async function fetchData(url) {
	try {
		const response = await fetch(url);
		const data = await response.json();
		return data;
	} catch (error) {
		console.error('Error fetching chart data:', error);
	}
}
(async function () {
	// Make an asynchronous AJAX request to fetch chart data
	const chartData = await fetchData('/manage/home/GetElectionVotesAsJson');
	console.log(chartData);

	// Use the returned data to populate the chart
	var electsName = chartData.labels;
	var votes = chartData.data;
	var data = {
		labels: electsName,
		datasets: [{
			label: 'Elections',
			backgroundColor: 'rgba(78, 115, 223, 0.05)',
			borderColor: 'rgba(78, 115, 223, 1)',
			data: votes, // Replace this with your actual data
		}],
	};

	// Chart configuration
	var config = {
		type: 'line',
		data: data,
		options: {
			maintainAspectRatio: false,
			scales: {
				x: [{
					grid: {
						display: false,
					},
				}],
				y: [{
					grid: {
						drawBorder: false,
						display: false,
					},
				}],
			},
			plugins: {
				legend: {
					display: false,
				},
			},
		},
	};

	// Get the canvas element
	var ctx = document.getElementById('ElectionChat').getContext('2d');

	// Create the chart with the provided data and configuration
	var ElectionChat = new Chart(ctx, config);
})();

