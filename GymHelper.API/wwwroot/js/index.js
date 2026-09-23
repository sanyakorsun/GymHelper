const userId = localStorage.getItem("userId");

if (userId) {
    loadStats();
}

async function loadStats() {
    try {
        const workoutsResponse = await fetch(`/api/workouts/user/${userId}`);
        const exercisesResponse = await fetch("/api/exercises");

        if (!workoutsResponse.ok || !exercisesResponse.ok) {
            throw new Error();
        }

        const workouts = await workoutsResponse.json();
        const exercises = await exercisesResponse.json();

        document.getElementById("workoutCount").textContent =
            workouts.length;

        document.getElementById("exerciseCount").textContent =
            exercises.length;

        if (workouts.length > 0) {
            const lastWorkout = new Date(workouts[0].date);

            document.getElementById("lastWorkout").textContent =
                lastWorkout.toLocaleDateString("uk-UA");
        }
    } catch {
        console.log("Не вдалося завантажити статистику");
    }
}