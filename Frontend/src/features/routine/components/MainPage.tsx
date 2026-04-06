import HabitBoard from "./HabitBoard";
import MenuNav from "../../../Components/MenuNav";
import { useState } from "react";
import styles from "./MainPage.module.css";
import Heatmap from "./Heatmap";

export default function MainPage() {
  const [user, setUser] = useState("sam");

  return (
    <div className={styles.page}>
      <MenuNav></MenuNav>
      <main className={styles.main}>
        <HabitBoard username={user} />
        <Heatmap></Heatmap>
      </main>
    </div>
  );
}
