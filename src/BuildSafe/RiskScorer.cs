namespace SafeDesignLite
{
    public static class RiskScorer
    {
        public static int Score(Hazard h)
        {
            int score = 0;

            if (h.Severity == "High") score += 70;
            if (h.Severity == "Medium") score += 40;
            if (h.Category.Contains("Fall")) score += 20;

            return score;
        }
    }
}