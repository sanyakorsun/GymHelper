async function loadExercises() {
    const container = document.getElementById("exercises");

    try {
        const response = await fetch(
            "/api/exercises"
        );

        if (!response.ok) {
            throw new Error("Не вдалося отримати вправи");
        }

        const exercises = await response.json();

        container.innerHTML = "";

        exercises.forEach(exercise => {
            const section = document.createElement("section");

            section.className = "workout";

            section.innerHTML = `
                <h2>${exercise.name}</h2>
                <p>Група м'язів: ${exercise.muscleGroup}</p>
                <p>${exercise.description || ""}</p>

                <button onclick="deleteExercise('${exercise.id}')">
                    Видалити
                </button>
`;

            container.appendChild(section);
        });

    } catch (error) {
        console.error(error);

        container.innerHTML = `
            <section class="workout">
                <h2>Помилка</h2>
                <p>Не вдалося завантажити вправи.</p>
            </section>
        `;
    }
}

async function createExercise() {
    const name = document.getElementById("exerciseName").value;
    const muscleGroup = document.getElementById("muscleGroup").value;
    const description = document.getElementById("description").value;

    const message = document.getElementById("exerciseMessage");

    if (!name || !muscleGroup) {
        message.textContent = "Заповни назву та групу м'язів";
        return;
    }

    try {
        const response = await fetch(
            "/api/exercises",
            {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    name: name,
                    muscleGroup: muscleGroup,
                    description: description
                })
            }
        );

        if (!response.ok) {
            throw new Error("Не вдалося створити вправу");
        }

        message.textContent = "Вправу додано!";

        document.getElementById("exerciseName").value = "";
        document.getElementById("muscleGroup").value = "";
        document.getElementById("description").value = "";

        await loadExercises();

    } catch (error) {
        console.error(error);

        message.textContent =
            "Не вдалося додати вправу";
    }
}

async function deleteExercise(id) {
    try {
        const response = await fetch(
            `https://localhost:7226/api/exercises/${id}`,
            {
                method: "DELETE"
            }
        );

        if (!response.ok) {
            throw new Error("Не вдалося видалити вправу");
        }

        await loadExercises();

    } catch (error) {
        console.error(error);

        document.getElementById("exerciseMessage").textContent =
            "Не вдалося видалити вправу";
    }
}

loadExercises();