import HabitBoard from "./HabitBoard";
import Heatmap from "./Heatmap";
import AuthPageLayout from "../../../components/AuthPageLayout";
import useUser from "../../../hooks/useUser";

export default function MainPage() {
  const { user } = useUser();

  console.log(user);

  return (
    <AuthPageLayout>
      <HabitBoard username={user?.username ?? ""} />
      <Heatmap></Heatmap>
    </AuthPageLayout>
  );
}
