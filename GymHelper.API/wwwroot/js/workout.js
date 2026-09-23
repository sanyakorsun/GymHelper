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

        localStorage.setItem("workoutId", workout.id);

        document.getElementById("exerciseSelect").disabled = false;
        document.getElementById("addExerciseButton").disabled = false;
        document.getElementById("finishWorkoutButton").disabled = false;

        document.getElementById("workoutMessage").textContent =
            "Тренування створено!";

    } catch (error) {
        console.error(error);

        document.getElementById("workoutMessage").textContent =
            "Не вдалося створити тренування";
    }
}


async function loadExercises() {
    try {
        const response = await fetch("/api/exercises");

        if (!response.ok) {
            throw new Error("Не вдалося отримати вправи");
        }

        const exercises = await response.json();

        const select = document.getElementById("exerciseSelect");

        select.innerHTML = '<option value="">Вибери вправу</option>';

        exercises.forEach(exercise => {
            const option = document.createElement("option");

            option.value = exercise.id;
            option.textContent = exercise.name;

            select.appendChild(option);
        });

    } catch (error) {
        console.error(error);

        document.getElementById("exerciseMessage").textContent =
            "Не вдалося завантажити вправи";
    }
}


async function addExercise() {
    const workoutId = localStorage.getItem("workoutId");
    const exerciseId = document.getElementById("exerciseSelect").value;
    const exerciseName =
        document.getElementById("exerciseSelect").selectedOptions[0].textContent;

    if (!workoutId) {
        document.getElementById("exerciseMessage").textContent =
            "Спочатку створи тренування";

        return;
    }

    if (!exerciseId) {
        document.getElementById("exerciseMessage").textContent =
            "Вибери вправу";

        return;
    }

    try {
        const response = await fetch("/api/workout-exercises", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                workoutId: workoutId,
                exerciseId: exerciseId
            })
        });

        if (!response.ok) {
            throw new Error("Не вдалося додати вправу");
        }

        const workoutExercise = await response.json();

        document.getElementById("exerciseMessage").textContent =
            "Вправу додано!";

        document.getElementById("exerciseSelect").value = "";

        createExerciseBlock(workoutExercise, exerciseName);

    } catch (error) {
        console.error(error);

        document.getElementById("exerciseMessage").textContent =
            "Не вдалося додати вправу";
    }
}


function createExerciseBlock(workoutExercise, exerciseName) {
    const container = document.getElementById("workoutExercises");

    const section = document.createElement("section");

    section.className = "workout";

    section.innerHTML = `
        <h2>${exerciseName}</h2>

        <div class="set-list"></div>

        <button onclick="addSet(this, '${workoutExercise.id}')">
            + Додати підхід
        </button>

        <p class="set-message"></p>
    `;

    container.appendChild(section);
}


function addSet(button, workoutExerciseId) {
    const section = button.parentElement;
    const setList = section.querySelector(".set-list");

    const setNumber = setList.children.length + 1;

    const set = document.createElement("div");

    set.className = "set";

    set.innerHTML = `
        <h3>Підхід ${setNumber}</h3>

        <input
            type="number"
            step="0.1"
            placeholder="Вага (кг)"
            class="set-weight"
        >

        <input
            type="number"
            placeholder="Повтори"
            class="set-reps"
        >

        <button onclick="saveSet(this, '${workoutExerciseId}')">
            Зберегти підхід
        </button>
    `;

    setList.appendChild(set);
}


async function saveSet(button, workoutExerciseId) {
    const set = button.parentElement;

    const weight = Number(set.querySelector(".set-weight").value);
    const reps = Number(set.querySelector(".set-reps").value);

    const message = set.parentElement.parentElement.querySelector(".set-message");

    if (!weight || !reps) {
        message.textContent =
            "Введи вагу та кількість повторів";

        return;
    }

    try {
        const response = await fetch("/api/sets", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                workoutExerciseId: workoutExerciseId,
                weight: weight,
                reps: reps
            })
        });

        if (!response.ok) {
            throw new Error("Не вдалося зберегти підхід");
        }

        const savedSet = await response.json();

        console.log("Збережений підхід:", savedSet);

        message.textContent =
            `Підхід збережено: ${savedSet.weight} кг × ${savedSet.reps}`;

        button.disabled = true;

    } catch (error) {
        console.error(error);

        message.textContent =
            "Не вдалося зберегти підхід";
    }
}

async function finishWorkout() {
    const workoutId = localStorage.getItem("workoutId");

    if (!workoutId) {
        document.getElementById("finishMessage").textContent =
            "Спочатку створи тренування";

        return;
    }

    try {
        const response = await fetch(
            `/api/workouts/${workoutId}/finish`,
            {
                method: "POST"
            }
        );

        const result = await response.text();

        if (!response.ok) {
            document.getElementById("finishMessage").textContent =
                result || "Додай хоча б один підхід";

            return;
        }


        document.getElementById("finishMessage").textContent =
            "Тренування завершено!";

        localStorage.removeItem("workoutId");

        setTimeout(() => {
            window.location.href = "history.html";
        }, 1000);


        document.getElementById("finishWorkoutButton").disabled = true;
        document.getElementById("exerciseSelect").disabled = true;
        document.getElementById("addExerciseButton").disabled = true;

    } catch (error) {
        console.error(error);

        document.getElementById("finishMessage").textContent =
            "Не вдалося завершити тренування";
    }
}

loadExercises();