export default function RegisterStepTwo({
  onBackForm,
  onSendForm,
}: {
  onBackForm: () => void;
  onSendForm: () => void;
}) {
  return (
    <form>
      <div>
        <label htmlFor="">Tu meta minima</label>
        <p>Lorem ipsum dolor sit amet consectetur.</p>
      </div>
      <button onClick={onBackForm}>Volver</button>
      <button onClick={onSendForm}>Enviar</button>
    </form>
  );
}
