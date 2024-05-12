import GSAFormCard from "./GSAFormCard.js";
import styles from "../styles/pagecontainer.module.css";

export default function PageContainer() {
  return (
    <div className={styles.pageContainer}>
      <GSAFormCard />
    </div>
  );
}
