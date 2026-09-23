async function loadHistory() {
    const userId = localStorage.getItem("userId");
    const history = document.getElementById("history");

    history.innerHTML = "";

    if (!userId) {
        history.innerHTML = `
            <section class="workout">
                <h2>Профіль не створено</h2>
                <p>Спочатку створи профіль.</p>
            </section>
        `;

        return;
    }

    try {
        const response = await fetch(
            `/api/workouts/user/${userId}`
        );

        if (!response.ok) {
            throw new Error("Не вдалося отримати історію");
        }

        const workouts = await response.json();

        if (workouts.length === 0) {
            history.innerHTML = `
                <section class="workout">
                    <h2>Історія порожня</h2>
                    <p>У тебе ще немає завершених тренувань.</p>
                </section>
            `;

            return;
        }

        workouts.forEach(workout => {
            createWorkoutBlock(workout);
        });

    } catch (error) {
        console.error(error);

        history.innerHTML = `
            <section class="workout">
                <h2>Помилка</h2>
                <p>Не вдалося завантажити історію тренувань.</p>
            </section>
        `;
    }
}


function createWorkoutBlock(workout) {
    console.log("Workout ID:", workout.id);

    const history = document.getElementById("history");

    const section = document.createElement("section");

    section.className = "workout";

    const date = new Date(workout.date).toLocaleDateString("uk-UA");

    section.innerHTML = `
    <div class="history-header">
        <h2>Тренування — ${date}</h2>

        <button class="delete-workout-button"
                onclick="deleteWorkout('${workout.id}')">
            Видалити тренування
        </button>
    </div>

    <div class="history-exercises"></div>
`;

    const exercisesContainer =
        section.querySelector(".history-exercises");

    workout.workoutExercises.forEach(workoutExercise => {
        const exercise = document.createElement("div");

        exercise.className = "set";

        exercise.innerHTML = `
            <h3>${workoutExercise.exercise.name}</h3>
        `;

        workoutExercise.sets.forEach((set, index) => {
            const setElement = document.createElement("p");

            setElement.textContent =
                `Підхід ${index + 1}: ${set.weight} кг × ${set.reps}`;

            exercise.appendChild(setElement);
        });

        exercisesContainer.appendChild(exercise);
    });

    history.appendChild(section);
}

async function deleteWorkout(workoutId) {
    const confirmed = confirm(
        "Точно видалити це тренування?"
    );

    if (!confirmed) {
        return;
    }

    try {
        const response = await fetch(
            `/api/workouts/${workoutId}`,
            {
                method: "DELETE"
            }
        );

        const result = await response.text();

        if (!response.ok) {
            console.error("DELETE error:", response.status, result);

            alert(
                `Не вдалося видалити тренування.\nКод: ${response.status}\n${result}`
            );

            return;
        }

        await loadHistory();

    } catch (error) {
        console.error(error);

        alert("Не вдалося підключитися до API");
    }
}

loadHistory();