import Header from "./Header";
import styles from "./MenuNav.module.css";

export default function MenuNav() {
  return (
    <Header>
      <ul className={styles.menu}>
        <li>
          <a href="" className={styles.menuLink}>
            Inicio
          </a>
        </li>
        <li>
          <a href="" className={styles.menuLink}>
            Tus tareas
          </a>
        </li>
        <li>
          <a href="" className={styles.menuLink}>
            Historial
          </a>
        </li>
      </ul>
    </Header>
  );
}
