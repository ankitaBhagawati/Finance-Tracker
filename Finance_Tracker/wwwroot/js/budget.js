// Budget categories
const categories = [
    { id: 1, name: 'Food', budget: 0 },
    { id: 2, name: 'Travel', budget: 0 },
    { id: 3, name: 'Bills', budget: 0 },
    { id: 4, name: 'Shopping', budget: 0 },
    { id: 5, name: 'Health', budget: 0 }
];

let selectedCategoryId = null;

$(() => {
    renderBudgetCards();
    renderBudgetCharts(); // Initial chart render

    $('#budgetForm').on('submit', function (e) {
        e.preventDefault();

        const amountRaw = $('#budgetAmount').val().trim();
        const amount = parseFloat(amountRaw);

        if (!amountRaw || isNaN(amount) || amount < 0) {
            alert('Please enter a valid budget amount.');
            return;
        }

        const category = categories.find(c => c.id === selectedCategoryId);
        if (category) category.budget = amount;

        $('#budgetModal').modal('hide');
        renderBudgetCards();
        renderBudgetCharts(); // Re-render chart after budget update
    });
});

function renderBudgetCards() {
    const container = $('#budgetCardsContainer');
    container.empty();

    categories.forEach(category => {
        const iconClass = category.budget > 0 ? 'bi-pencil-square' : 'bi-plus-lg';
        const btnTitle = category.budget > 0 ? 'Edit Budget' : 'Add Budget';

        const card = `
            <div class="col-md-4 mb-3">
                <div class="card shadow-sm position-relative">
                    <div class="card-body text-center">
                        <h5 class="card-title">${category.name}</h5>
                        <p class="card-text fs-4">₹${category.budget}</p>
                        <button class="btn btn-light position-absolute top-0 end-0 m-2" title="${btnTitle}" onclick="openBudgetModal(${category.id})">
                            <i class="bi ${iconClass}"></i>
                        </button>
                    </div>
                </div>
            </div>
        `;
        container.append(card);
    });
}

function openBudgetModal(categoryId) {
    selectedCategoryId = categoryId;
    const category = categories.find(c => c.id === categoryId);
    $('#modalCategoryText').text(`Enter your budget for "${category.name}"`);
    $('#budgetAmount').val(category.budget || '');
    $('#budgetModal').modal('show');
}

function renderBudgetCharts() {
    const container = $('#budgetChartsContainer');
    container.empty();

    categories.forEach(category => {
        const spent = expenses.reduce((sum, exp) => (exp.Category === category.id ? sum + exp.Amount : sum), 0);
        const budgetAmount = category.budget || 0;

        // Create a container div for each ring
        const ringId = `progress-ring-${category.id}`;
        container.append(`
          <div style="display: inline-block; width: 150px; margin: 15px; text-align: center; position: relative;">
            <div id="${ringId}"></div>
            <div style="margin-top: 10px; font-weight: bold;">${category.name}</div>
            <div>Spent: ₹${spent.toFixed(2)}</div>
            <div>Budget: ₹${budgetAmount.toFixed(2)}</div>
          </div>
        `);

        // Calculate progress (cap at 1 for 100%)
        let progress = budgetAmount === 0 ? 0 : Math.min(spent / budgetAmount, 1);

        // Decide color and text color based on progress ranges
        let color, textColor;
        if (progress === 0) {
            color = '#bbb';        // grey stroke when no budget/spent
            textColor = '#bbb';    // grey text
        } else if (progress <= 0.5) {
            color = '#2ecc40 ';     // green
            textColor = '#666600';
        } else if (progress <= 0.8) {
            color = '#ffeb3b';     // yellow
            textColor = '#666600'; // darker yellow text for readability
        } else {
            color = '#f44336';     // red
            textColor = '#666600';
        }

        // Create circular progress bar
        let bar = new ProgressBar.Circle(`#${ringId}`, {
            color: color,
            strokeWidth: 12,
            trailWidth: 8,
            easing: 'easeInOut',
            duration: 1400,
            text: {
                value: '0%',
                style: {
                    color: textColor,
                    position: 'absolute',
                    left: '50%',
                    top: '50%',
                    padding: 0,
                    margin: 0,
                    transform: 'translate(-50%, -50%)',
                    'font-size': '1.4rem',
                    'font-weight': 'bold',
                    'user-select': 'none',
                },
                autoStyleContainer: false
            },
            from: { color: '#bbb', width: 8 },
            to: { color: color, width: 12 },
            step: (state, circle) => {
                circle.path.setAttribute('stroke', state.color);
                circle.path.setAttribute('stroke-width', state.width);

                const percent = Math.round(circle.value() * 100);
                circle.setText(`${percent}%`);
                circle.text.style.color = textColor;
            }
        });

        bar.animate(progress);  // animate to progress value
    });
}

