window.renderChart = function (canvasId, chartConfig) {
    const canvas = document.getElementById(canvasId);
    if (!canvas) {
        console.error('Canvas element not found:', canvasId);
        return;
    }

    // Destroy existing chart if it exists
    if (window.charts && window.charts[canvasId]) {
        window.charts[canvasId].destroy();
    }

    // Initialize charts object if it doesn't exist
    if (!window.charts) {
        window.charts = {};
    }

    // Create new chart
    const ctx = canvas.getContext('2d');
    window.charts[canvasId] = new Chart(ctx, chartConfig);
}; 