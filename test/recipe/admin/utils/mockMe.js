export default {
  async theSession(page, { access, refresh }) {
    await page.evaluate(() => localStorage.removeItem("token"));
    await page.evaluate(token => localStorage.setItem("token", token), JSON.stringify({ access, refresh }));
  }
};
