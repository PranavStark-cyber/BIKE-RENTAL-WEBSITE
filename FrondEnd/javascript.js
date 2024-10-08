// document.addEventListener('DOMContentLoaded', function () {
//     const rentContainer = document.getElementById('rent-container');

//     const bikes = JSON.parse(localStorage.getItem('bikes')) || [];

//     function createBikeCard(bike) {
//         const bikeBox = document.createElement('div');
//         bikeBox.classList.add('rent-box');

//         bikeBox.innerHTML = `
//             <img src="${bike.image}" alt="${bike.title}">
//             <div class="rent-layer">
//                 <h4>${bike.title}</h4>
//                 <p>${bike.description}</p>
//                  <p>${bike.model}</p>
//                   <p>${bike.brand}</p>
//                    <p>${bike.category}</p>   
//                 <a href="login.html"><i class='bx bx-link-external'></i></a>
//             </div>
    
//         `;

//         return bikeBox;
//     }

//     function displayBikes() {
//         bikes.forEach(bike => {
//             const bikeCard = createBikeCard(bike);
//             rentContainer.appendChild(bikeCard);
//         });
//     }

//     displayBikes();
// });

document.addEventListener('DOMContentLoaded', async function () {
    const rentContainer = document.getElementById('rent-container');

    async function fetchBikes() {
        try {
            const response = await fetch('http://localhost:5000/api/Manager/AllBikes');
            const bikes = await response.json();
            return bikes;
        } catch (error) {
            console.error('Error fetching bikes:', error);
            return [];
        }
    }

    function createBikeCard(bike) {
        const bikeBox = document.createElement('div');
        bikeBox.classList.add('rent-box');
        const imageUrls = bike.imageUrl.split(',');
        let fullUrl = '';
        imageUrls.forEach(url => {
             fullUrl += `http://localhost:5000${url}`.trim();
            console.log(fullUrl);
        });
        bikeBox.innerHTML = `
            <img src="${fullUrl}" alt="${bike.model}">
            <div class="rent-layer">
                <h4>${bike.title}</h4>
                <p>${bike.description}</p>
                <p>${bike.model}</p>
                <p>${bike.brand}</p>
                <p>${bike.category}</p>   
                <a href="login.html"><i class='bx bx-link-external'></i></a>
            </div>
        `;

        return bikeBox;
    }

    async function displayBikes() {
        const bikes = await fetchBikes();

        bikes.forEach(bike => {
            const bikeCard = createBikeCard(bike);
            rentContainer.appendChild(bikeCard);
        });
    }

    displayBikes();
});


