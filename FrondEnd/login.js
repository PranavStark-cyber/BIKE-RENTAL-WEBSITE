document.addEventListener('DOMContentLoaded', async function () {
    const loginForm = document.getElementById('loginForm');
    const registerForm = document.getElementById('registerForm');
    const logoutBtn = document.getElementById('logoutBtn');
    const userInfo = document.getElementById('userInfo');

    let users = [];
    let motorbikes = [];
    let rentals = [];

    // Fetch users from API
    async function fetchUsers() {
        try {
            const response = await fetch('http://localhost:5000/api/Customer/all');
            users = await response.json();
        } catch (error) {
            console.error('Error fetching users:', error);
        }
    }

    // Fetch motorbikes from API
    async function fetchMotorbikes() {
        try {
            const response = await fetch('http://localhost:5000/api/Manager/AllBikes');
            motorbikes = await response.json();
        } catch (error) {
            console.error('Error fetching motorbikes:', error);
        }
    }

    // Fetch rentals from API
    async function fetchRentals() {
        try {
            const response = await fetch('http://localhost:5000/api/Customer/rentals');
            rentals = await response.json();
        } catch (error) {
            console.error('Error fetching rentals:', error);
        }
    }

    // Initialize data on page load
    await Promise.all([fetchUsers(), fetchMotorbikes(), fetchRentals()]);

    const currentUser = JSON.parse(sessionStorage.getItem('currentUser'));
    if (currentUser) {
        if (currentUser.role === 'customer') {
            window.location.href = './customer-page/customer.html';
        }
    }

    // Login form submission
    if (loginForm) {
        loginForm.addEventListener('submit', async function (e) {
            e.preventDefault();
            const username = document.getElementById('loginUsername').value;
            const password = document.getElementById('loginPassword').value;
console.log(users);

            const user = users.find(u => u.firstName == username && u.password == password);
            if (user) {
                sessionStorage.setItem('currentUser', JSON.stringify(user));
                window.location.href = './customer-page/customer.html';
            } else {
                alert('Invalid credentials');
            }
        });
    }

    // Register form submission with API call
    if (registerForm) {
        registerForm.addEventListener('submit', async function (e) {
            e.preventDefault();
            const username = document.getElementById('registerUsername').value;
            const password = document.getElementById('registerPassword').value;
            const nic = document.getElementById('registerNIC').value;
            const number = document.getElementById('registernumber').value;
            const licence = document.getElementById('registerlicence').value;
    
            const userData = {
                firstName: username,
                mobilenumber: number,
                licence: licence,
                nic: nic,
                password: password
            };
    
            // Check if the username already exists
            if (users.some(u => u.username == username)) {
                alert('Username already exists');
                return;
            }
    
            try {
                // Send POST request to register customer
                const response = await fetch('http://localhost:5000/api/Customer', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(userData)
                });
    
                if (response.ok) {
                    await fetchUsers();
                    alert('Registration successful. Please login.');
                    registerForm.reset();
                } else {
                    const errorMessage = await response.json();
                    alert(`Registration failed: ${errorMessage.message}`);
                }
            } catch (error) {
                console.error('Error registering user:', error);
            }
        });
    }
    
    // Logout functionality
    if (logoutBtn) {
        logoutBtn.addEventListener('click', function () {
            sessionStorage.removeItem('currentUser');
            window.location.href = '../bikerent-greetingpage.html';
        });
    }

    // Display user info
    if (userInfo && currentUser) {
        userInfo.textContent = `${currentUser.username}`;
    }

    // Customer functionality (if on customer page)
    if (window.location.pathname.includes('./customer-page/customer.html')) {
        const availableMotorbikeBody = document.getElementById('rent-container');
        const myRentalsTableBody = document.getElementById('myRentalsTableBody');

        // Display available motorbikes
        function displayAvailableMotorbikes() {
            availableMotorbikeBody.innerHTML = '';
            motorbikes.forEach((motorbike) => {
                if (!isMotorbikeRented(motorbike.regNumber)) {
                    const bikeBox = document.createElement('div');
                    bikeBox.classList.add('rent-box');
                    const imageUrls = bike.imageUrl.split(',');
                    let fullUrl = '';
                    imageUrls.forEach(url => {
                        fullUrl += `http://localhost:5000${url}`.trim();
                        console.log(fullUrl);
                    });
                    bikeBox.innerHTML = `
                         <img src="${fullUrl}" alt="${motorbikes.model}">
                        <div class="rent-layer">
                            <h4>${motorbike.title}</h4>
                            <p>${motorbike.description}</p>
                            <p>${motorbike.model}</p>
                            <p>${motorbike.brand}</p>
                            <p>${motorbike.category}</p>
                            <a href="#" onclick="rentMotorbike('${motorbike.regNumber}')" ><i class='bx bx-link-external'>Rent</i></a>
                        </div>
                    `;
                    availableMotorbikeBody.appendChild(bikeBox);
                }
            });
        }

        // Check if motorbike is rented
        function isMotorbikeRented(regNumber) {
            return rentals.some(rental => rental.regNumber === regNumber);
        }

        // Rent motorbike
        window.rentMotorbike = async function (regNumber) {
            const rental = {
                regNumber,
                username: currentUser.username,
                rentDate: new Date().toLocaleDateString()
            };
            rentals.push(rental);
            // Assuming you use the API to manage rentals
            displayAvailableMotorbikes();
            displayMyRentals();
        };

        // Display user's rentals
        function displayMyRentals() {
            myRentalsTableBody.innerHTML = '';
            rentals.forEach((rental) => {
                if (rental.username === currentUser.username) {
                    const motorbike = motorbikes.find(mb => mb.regNumber === rental.regNumber);
                    const row = document.createElement('tr');
                    row.innerHTML = `
                        <td>${motorbike.regNumber}</td>
                        <td>${motorbike.brand}</td>
                        <td>${motorbike.model}</td>
                        <td>${motorbike.category}</td>
                        <td>${rental.rentDate}</td>
                        <td><button class="btn btn-danger btn-sm" onclick="returnMotorbike('${rental.regNumber}')">Return</button></td>
                    `;
                    myRentalsTableBody.appendChild(row);
                }
            });
        }

        // Return motorbike
        window.returnMotorbike = async function (regNumber) {
            rentals = rentals.filter(rental => rental.regNumber !== regNumber);
            // Assuming you use the API to manage returns
            displayMyRentals();
            displayAvailableMotorbikes();
        };

        displayAvailableMotorbikes();
        displayMyRentals();
    }
});
