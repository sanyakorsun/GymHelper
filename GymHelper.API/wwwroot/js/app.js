
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

                document.getElementById("message").textContent =
                    `Профіль ${user.name} успішно створено!`;

        document.getElementById("name").value = "";
        document.getElementById("age").value = "";
        document.getElementById("weight").value = "";
        document.getElementById("height").value = "";

    } catch (error) {
        console.error(error);

        document.getElementById("message").textContent =
            "Не вдалося створити профіль";
    }
}

async function createWorkout() {
    const userId = localStorage.getItem("userId");
    const date = document.getElementById("workoutDate").value;

    if (!userId) {
        document.getElementById("workoutMessage").textContent =
            "Спочатку створи профіль";

        return;
    }

    if (!date) {
        document.getElementById("workoutMessage").textContent =
            "Вибери дату тренування";

        return;
    }

    try {
        const response = await fetch("/api/workouts", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                userId: userId,
                date: date
            })
        });

        if (!response.ok) {
            throw new Error("Помилка створення тренування");
        }

        const workout = await response.json();

        document.getElementById("workoutMessage").textContent =
            "Тренування успішно створено!";

        document.getElementById("workoutDate").value = "";

        console.log(workout);

    } catch (error) {
        console.error(error);

        document.getElementById("workoutMessage").textContent =
            "Не вдалося створити тренування";
    }
}