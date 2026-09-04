export default function InputTime({ symbol }: { symbol: string }) {
  return (
    <div>
      <input type="text" pattern="[0-9]*" inputMode="numeric"></input>
      <label htmlFor="">{symbol}</label>
    </div>
  );
}
