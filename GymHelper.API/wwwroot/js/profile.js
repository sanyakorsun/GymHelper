const apiUrl = "/api/users";

let userId = localStorage.getItem("userId");

if (userId) {
    loadProfile();
}

async function loadProfile() {
    try {
        const response = await fetch(`${apiUrl}/${userId}`);

        if (!response.ok) {
            throw new Error();
        }

        const user = await response.json();

        document.getElementById("name").value = user.name;
        document.getElementById("age").value = user.age;
        document.getElementById("weight").value = user.weight;
        document.getElementById("height").value = user.height;

        document.getElementById("message").textContent =
            "Профіль завантажено";
    } catch {
        document.getElementById("message").textContent =
            "Не вдалося завантажити профіль";
    }
}

async function saveProfile() {
    const name = document.getElementById("name").value.trim();
    const age = Number(document.getElementById("age").value);
    const weight = Number(document.getElementById("weight").value);
    const height = Number(document.getElementById("height").value);

    if (!name) {
        document.getElementById("message").textContent =
            "Введіть ім'я";
        return;
    }

    if (age < 1 || age > 120) {
        document.getElementById("message").textContent =
            "Введіть справжній вік";
        return;
    }

    if (weight < 1 || weight > 300) {
        document.getElementById("message").textContent =
            "Введіть справжню вагу";
        return;
    }

    if (height < 50 || height > 250) {
        document.getElementById("message").textContent =
            "Введіть справжній зріст";
        return;
    }

    const user = {
        name: name,
        age: age,
        weight: weight,
        height: height
    };

    if (!userId) {
        await createUser(user);
        return;
    }

    try {
        const response = await fetch(`${apiUrl}/${userId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(user)
        });

        if (!response.ok) {
            throw new Error();
        }

        document.getElementById("message").textContent =
            "Профіль успішно оновлено";
    } catch {
        document.getElementById("message").textContent =
            "Помилка при оновленні профілю";
    }
}

async function createUser(user) {
    try {
        const response = await fetch(apiUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(user)
        });

        if (!response.ok) {
            throw new Error();
        }

        const createdUser = await response.json();

        userId = createdUser.id;

        localStorage.setItem("userId", userId);

        document.getElementById("message").textContent =
            "Профіль успішно створено";
    } catch {
        document.getElementById("message").textContent =
            "Помилка при створенні профілю";
    }
}