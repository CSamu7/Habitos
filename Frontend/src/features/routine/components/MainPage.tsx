import HabitBoard from "./HabitBoard";
import { useState } from "react";
import Heatmap from "./Heatmap";
import AuthPageLayout from "../../../Components/AuthPageLayout";

export default function MainPage() {
  const [user, setUser] = useState("sam");

  return (
    <AuthPageLayout>
      <HabitBoard username={user} />
      <Heatmap></Heatmap>
    </AuthPageLayout>
  );
}
