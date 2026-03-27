import { useState } from "react";
import styles from "./Login.module.css";
import Header from "../../../Components/Header";
import { LOGIN_URL } from "../api";

export default function Login() {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null>(null);

  interface LoginUserData {
    username: string;
    password: string;
  }

  const handleSubmit = async () => {
    setError(null);

    if (!username || !password) {
      setError("Por favor ingresa todos los campos");
      return;
    }

    const data: LoginUserData = { username, password };

    const result = await fetch(LOGIN_URL, {
      method: "POST",
      body: JSON.stringify(data),
      headers: {
        "Content-type": "application/json",
      },
    });
  };

  return (
    <div className={styles.page}>
      <Header>PORQUE</Header>

      <main className={styles.main}>
        <div className={styles.card}>
          <div className={styles.cardHeader}>
            <h1 className={styles.title}>INICIO DE SESIÓN</h1>
            <p className={styles.subtitle}>Ingresa tus datos</p>
          </div>

          <div className={styles.cardBody}>
            <div className={styles.fieldGroup}>
              <label className={styles.label}>Nombre de usuario</label>
              <input
                type="email"
                value={username}
                onChange={(e) => setUsername(e.target.value)}
                className={styles.input}
              />
            </div>

            <div className={styles.fieldGroup}>
              <label className={styles.label}>Contraseña</label>
              <input
                type="password"
                value={password}
                onChange={(e) => setPassword(e.target.value)}
                className={styles.input}
              />
            </div>

            {error && (
              <div className={`${styles.btn} ${styles.btnError}`}>{error}</div>
            )}

            <button
              onClick={handleSubmit}
              className={`${styles.btn} ${error ? styles.btnSubmit : styles.btnSubmitOnly}`}
            >
              Continuar
            </button>
          </div>
        </div>
      </main>
    </div>
  );
}
