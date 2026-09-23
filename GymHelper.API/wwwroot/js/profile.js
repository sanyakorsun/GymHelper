const apiUrl = "/api/users";

async function createUser() {
    const name = document.getElementById("name").value;
    const age = Number(document.getElementById("age").value);
    const weight = Number(document.getElementById("weight").value);
    const height = Number(document.getElementById("height").value);

    if (!name || !age || !weight || !height) {
        document.getElementById("message").textContent =
            "Заповни всі поля";

        return;
    }

    try {
        const response = await fetch(apiUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                name: name,
                age: age,
                weight: weight,
                height: height
            })
        });

        if (!response.ok) {
            throw new Error("Помилка створення профілю");
        }

        const user = await response.json();

        localStorage.setItem("userId", user.id);

        document.getElementById("message").textContent =
            `Профіль ${user.name} успішно створено!`;

    } catch (error) {
        console.error(error);

        document.getElementById("message").textContent =
            "Не вдалося створити профіль";
    }
}