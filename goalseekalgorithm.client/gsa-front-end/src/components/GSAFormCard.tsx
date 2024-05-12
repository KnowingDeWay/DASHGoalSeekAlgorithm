import GSAForm from "./GSAForm";
import styles from "../styles/gsaformcard.module.css";

export default function GSAFormCard() {
  return (
    <div className={styles.cardContainer}>
      <h2 className={styles.formHeading}>Goal Seek Algorithm Calculator</h2>
      <GSAForm />
    </div>
  );
}
