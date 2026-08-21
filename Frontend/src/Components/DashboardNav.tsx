import NavMenu from "./NavMenu";

export default function DashboardNav() {
  return (
    <NavMenu>
      <li>
        <a href="/dashboard">Inicio</a>
      </li>
      <li>
        <a href="/my-routines">Mis rutinas</a>
      </li>
      <li>
        <a href="/stats">Historial</a>
      </li>
    </NavMenu>
  );
}
